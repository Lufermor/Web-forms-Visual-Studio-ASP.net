<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>
	
	<xsl:template match="Personas">
		<Personas>
			<xsl:apply-templates/>
		</Personas>
	</xsl:template>

	<xsl:template match="Persona">
		<Persona>
			<xsl:for-each select="*">
				<xsl:attribute name="{name()}">
					<xsl:value-of select="text()" />
				</xsl:attribute>
			</xsl:for-each>
		</Persona>
	</xsl:template>
</xsl:stylesheet>
