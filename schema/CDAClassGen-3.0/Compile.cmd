CLS
REM Compile statement for c#
SET XSDTOOL="c:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\XSD.exe"
del *.cs
%XSDTOOL% CDA-AU-V1_0.xsd EXTENSION.xsd /c /l:CS /n:Nehta.HL7.CDA
ren CDA-AU-V1_0_EXTENSION.cs hl7-cda-nehta-3_0_0.cs
pause
