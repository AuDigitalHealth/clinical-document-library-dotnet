using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DigitalHealth.HL7.Common
{
    public class HL7SegmentGroup
    {
        /// <summary>
        /// Encode segment group according to HL7 encoding rules with specified separators.
        /// </summary>
        /// <param HL7Name="seps">Characters to separate segments, fields, field repeats, components and subcomponents</param>
        /// <param HL7Name="sb">String builder</param>
        public void Encode(HL7Separators seps, StringBuilder sb)
        {
            var segmentGroupFields = GetType().GetFields().OrderBy(f => f.MetadataToken);
            foreach (FieldInfo segmentGroupField in segmentGroupFields)
            {
                if (segmentGroupField.FieldType.IsArray)
                {
                    Array array = segmentGroupField.GetValue(this) as Array;
                    foreach (object segmentGroupObject in array)
                    {
                        // Each item in the array could be another group of segments or a single segment

                        if (segmentGroupObject is HL7SegmentGroup)
                        {
                            HL7SegmentGroup segmentGroup = segmentGroupObject as HL7SegmentGroup;
                            segmentGroup.Encode(seps, sb);
                        }
                        else
                        {
                            HL7Segment segment = segmentGroupObject as HL7Segment;
                            segment.Encode(seps, sb);
                            sb.Append(seps.SegmentSeparator);
                        }
                    }
                }
                else
                {
                    // The non-repeating item could be a another group of segments, or a single segment, or null

                    object segmentGroupObject = segmentGroupField.GetValue(this);
                    if (segmentGroupObject is HL7SegmentGroup)
                    {
                        HL7SegmentGroup segmentGroup = segmentGroupObject as HL7SegmentGroup;
                        segmentGroup.Encode(seps, sb);
                    }
                    else if (segmentGroupObject != null)
                    {
                        HL7Segment segment = segmentGroupObject as HL7Segment;
                        segment.Encode(seps, sb);
                        sb.Append(seps.SegmentSeparator);
                    }
                }
            }
        }

        /// <summary>
        /// Create a segment group of the given identifier and consume the required segments into it.
        /// </summary>
        /// <param HL7Name="identifier">This identifier should inherit from HL7SegmentGroup</param>
        /// <param HL7Name="segments">Array of parsed segments</param>
        /// <param HL7Name="segmentIndex">Index of next segment that has not yet been consumed</param>
        internal static object BuildSegmentGroup(Type type, HL7Segment[] segments, ref int segmentIndex)
        {
            object segmentGroup = type.GetConstructor(System.Type.EmptyTypes).Invoke(null);
            IOrderedEnumerable<FieldInfo> subSegmentGroupFields = type.GetFields().OrderBy(f => f.MetadataToken);
            foreach (FieldInfo subSegmentGroupField in subSegmentGroupFields)
            {
                object subSegmentGroup;
                if (subSegmentGroupField.FieldType.IsArray)
                {
                    // Determine how many segments (maybe none) have the identifier required for the current group
                    Type arrayElementType = subSegmentGroupField.FieldType.GetElementType();
                    List<object> items = new List<object>();
                    if (arrayElementType.IsSubclassOf(typeof(HL7SegmentGroup)))
                    {
                        // The array element identifier is itself a segment group, recurse to consume the segments to build that segment group.
                        // Keep attempting to build until no more segments are consumed (segmentIndex does not change) or all the segments are used.
                        int newSegmentIndex = segmentIndex;
                        do
                        {
                            segmentIndex = newSegmentIndex;
                            object group = BuildSegmentGroup(arrayElementType, segments, ref newSegmentIndex);
                            if (segmentIndex != newSegmentIndex)
                            {
                                items.Add(group);
                            }
                        }
                        while (segmentIndex != newSegmentIndex && newSegmentIndex != segments.Length);
                    }
                    else
                    {
                        // The array element identifier is a single segment, consume all consecutive segments of that identifier
                        while (segmentIndex < segments.Length && segments[segmentIndex].GetType().Equals(arrayElementType))
                        {
                            items.Add(segments[segmentIndex++]);
                        }
                    }

                    // Store the items in an array
                    Array array = Array.CreateInstance(arrayElementType, items.Count);
                    for (int i = 0; i < items.Count; i++)
                    {
                        array.SetValue(items[i], i);
                    }
                    subSegmentGroup = array;
                }
                else
                {
                    if (subSegmentGroupField.FieldType.IsSubclassOf(typeof(HL7SegmentGroup)))
                    {
                        // The non-array field is a segment group, this is unusual (there's no point structuring a message this way) but we will support it
                        subSegmentGroup = BuildSegmentGroup(subSegmentGroupField.FieldType, segments, ref segmentIndex);
                    }
                    else if (subSegmentGroupField.FieldType.Equals(segments[segmentIndex].GetType()))
                    {
                        // The current segment has the identifier required at this point, so consume this segment.
                        subSegmentGroup = segments[segmentIndex++];
                    }
                    else
                    {
                        // Skip over this segment group, nothing matched, hope it was an optional one.
                        subSegmentGroup = null;
                    }
                }
                subSegmentGroupField.SetValue(segmentGroup, subSegmentGroup);
                if (segmentIndex == segments.Length)
                {
                    // The last segment has been consumed
                    break;
                }
            }

            // Don't care if not all the fields (or even none) were assigned.
            return segmentGroup;
        }
    }
}