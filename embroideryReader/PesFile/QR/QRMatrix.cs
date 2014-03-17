using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml.Linq;
using System.IO;
using System.Drawing;

namespace EmbroideryFile
{
    
    public class QRMatrix//: Matrix<int>
    {

        const string XSL_QRCODE_TABLE_TO_MATRIX = @"<xsl:stylesheet xmlns:xsl='http://www.w3.org/1999/XSL/Transform' version='1.0' >
  <xsl:template match='tbody'>
    <matrix>
      <xsl:apply-templates  />
    </matrix>
  </xsl:template>
 <xsl:template match='tr'><row><xsl:apply-templates/></row>
 </xsl:template>
       <xsl:template match='td'>
      <xsl:element name='cell'>        
        <xsl:choose>
          <xsl:when test=""contains(./@style,'rgb(0, 0, 0)')>0"">1</xsl:when>
          <xsl:otherwise>0</xsl:otherwise>
        </xsl:choose>        
      </xsl:element>        
        </xsl:template>      
  </xsl:stylesheet>";

        public  bool [][] Matrix;
        public int Dimension { get { if (Matrix != null) return Matrix.GetUpperBound(0) + 1; else return 0; } }
        
        //protected int dX { get; set; }
        //protected int dY { get; set; }
        //protected int cellSize { get; set; }
        
        #region [Constructor]

        public QRMatrix()
        {
            
        }
        public QRMatrix(string qrTableXml)
        {
            //XDocument which contains QR Code HTML table
            XDocument xDoc = XDocument.Parse(qrTableXml);

            XDocument xMatrix = new XDocument();
            using (XmlWriter writer = xMatrix.CreateWriter())
            {
                XslCompiledTransform transform = new XslCompiledTransform();
                transform.Load(XmlReader.Create(new StringReader(XSL_QRCODE_TABLE_TO_MATRIX)));
                transform.Transform(xDoc.CreateReader(), writer);

            };
            //Fills Jagged 2D array
            Matrix = ((from row in xMatrix.Descendants("row")
                       select (from cell in row.Elements("cell")
                               select (bool.Parse(cell.Value))).ToArray()).ToArray());

          

        }

        #endregion [Constructor]
  



    }






}
