# Código Visual FoxPro - Configuración de Guía de Remisión

Este archivo contiene el fragmento de código VFP para la configuración del control `lpinfo` y la consulta SQL de reporte.

```foxpro
lpinfo.Parent.cboFiltro02.visible = .T.
lpinfo.Parent.cboFiltro01.visible = .T.

lpinfo.columncount = 5

lpinfo.Column1.header1.caption = "Creación Guía"
lpinfo.Column1.width = 100
lpinfo.Column2.header1.caption = "Guía de Remisión"
lpinfo.Column2.width = 100
lpinfo.Column3.header1.caption = "Orden Ciateite"
lpinfo.Column3.width = 100
lpinfo.Column4.header1.caption = "Factura"
lpinfo.Column4.width = 100
lpinfo.Column5.header1.caption = "Cliente"
lpinfo.Column5.width = 350

lcsql = "EXEC TRA_GUIAS_REMISION_X_FACTURA '" + dtoc(ldstartdate,1) + "' , '" + dtoc(ldenddate,1) + "'"
lpinfo.recordsource = 'sqlexec(_dobra.sqlserverid,"' + lcsql + '","reporte")'
```
