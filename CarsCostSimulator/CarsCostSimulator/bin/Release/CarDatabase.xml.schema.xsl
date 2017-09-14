<?xml version="1.0" standalone="yes"?>
<xs:schema id="database" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="database" msdata:IsDataSet="true" msdata:Locale="en-US">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Car">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="consumption" type="xs:float" minOccurs="0" msdata:Ordinal="0" />
              <xs:element name="driver" type="xs:string" minOccurs="0" msdata:Ordinal="1" />
              <xs:element name="fuel" type="xs:string" minOccurs="0" msdata:Ordinal="2" />
              <xs:element name="insuranceprice" type="xs:float" minOccurs="0" msdata:Ordinal="3" />
              <xs:element name="name" type="xs:string" minOccurs="0" msdata:Ordinal="4" />
              <xs:element name="price" type="xs:float" minOccurs="0" msdata:Ordinal="5" />
              <xs:element name="tanksize" type="xs:float" minOccurs="0" msdata:Ordinal="6" />
              <xs:element name="Wearpart" minOccurs="0" maxOccurs="unbounded">
                <xs:complexType>
                  <xs:sequence>
                    <xs:element name="name" type="xs:string" minOccurs="0" msdata:Ordinal="0" />
                    <xs:element name="periodicity" type="xs:float" minOccurs="0" msdata:Ordinal="1" />
                    <xs:element name="price" type="xs:float" minOccurs="0" msdata:Ordinal="2" />
                  </xs:sequence>
                  <xs:attribute name="id" type="xs:string" />
                </xs:complexType>
              </xs:element>
            </xs:sequence>
            <xs:attribute name="id" type="xs:string" />
          </xs:complexType>
        </xs:element>
        <xs:element name="Fuel">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="name" type="xs:string" minOccurs="0" msdata:Ordinal="0" />
              <xs:element name="price" type="xs:float" minOccurs="0" msdata:Ordinal="1" />
            </xs:sequence>
            <xs:attribute name="id" type="xs:string" />
          </xs:complexType>
        </xs:element>
        <xs:element name="Driver">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="drivertype" type="xs:string" minOccurs="0" msdata:Ordinal="0" />
              <xs:element name="kmperyear" type="xs:float" minOccurs="0" msdata:Ordinal="1" />
              <xs:element name="maxkilometers" type="xs:float" minOccurs="0" msdata:Ordinal="2" />
              <xs:element name="name" type="xs:string" minOccurs="0" msdata:Ordinal="3" />
            </xs:sequence>
            <xs:attribute name="id" type="xs:string" />
          </xs:complexType>
        </xs:element>
        <xs:element name="Drivertype">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="drivingcoefficient" type="xs:float" minOccurs="0" msdata:Ordinal="0" />
              <xs:element name="name" type="xs:string" minOccurs="0" msdata:Ordinal="1" />
            </xs:sequence>
            <xs:attribute name="id" type="xs:string" />
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
  </xs:element>
</xs:schema>