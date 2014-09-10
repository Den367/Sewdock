using System;
using ContourLibrary;
using ContourLibrary.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestContourLibrary
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void UnitTestMethod1()
        {
             XmlContourLoader loader = new XmlContourLoader();
            ContourSearchInfo searchInfo = new ContourSearchInfo()
                {
                    Delimiter = ',',
                    FilePath = @"D:\WEB_PRJ\Mayando.Web\Content\Xml\country-contours.xml"
                    ,
                    IteratedElementName = "trk",
                    PolygonElementName = "poly",
                    SearchAttributeName = "name",
                    SearchAttributeValue = "Alaska",StartElementName = "xml"
                };
            var contour = loader.LoadContourFromFile(searchInfo);
            Assert.IsInstanceOfType(contour,typeof(ContourPolygons));
           
        }
    }
}
