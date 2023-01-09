<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>
	
	<xsl:template match="Facturas">
		<Factura>
			<xsl:apply-templates/>
		</Factura>
	</xsl:template>

	<xsl:template match="Factura">
		<Factura>
			<xsl:for-each select="*">
				<xsl:attribute name="{name()}">
					<xsl:value-of select="text()" />
				</xsl:attribute>
			</xsl:for-each>
		</Factura>
	</xsl:template>
</xsl:stylesheet>
