using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WaterHeaterTest.Models
{
    [Serializable]
    [XmlRoot(ElementName ="TEST-RESULTS")]
    public class TEST_RESULTS
    {
        public int Id { get; set; }

        [XmlElement(ElementName ="COMMENT1")]
        public string COMMENT1 { get; set; }


        [XmlElement(ElementName = "COMMENT2")]
        public string COMMENT2 { get; set; }


        [XmlElement(ElementName = "DEVICENAME")]
        public string DEVICENAME { get; set; }


        [XmlElement(ElementName = "DEVICEVALUE")]
        public string DEVICEVALUE { get; set; }


        [XmlElement(ElementName = "DEVNR")]
        public string DEVNR { get; set; }


        [XmlElement(ElementName = "ENDDATE")]
        public string ENDDATE { get; set; }


        [XmlElement(ElementName = "ERRCODE")]
        public string ERRCODE { get; set; }


        [XmlElement(ElementName = "MTXMINUS")]
        public string MTXMINUS { get; set; }


        [XmlElement(ElementName = "MTXMINUSSTR")]
        public string MTXMINUSSTR { get; set; }


        [XmlElement(ElementName = "MTXPLUS")]
        public string MTXPLUS { get; set; }


        [XmlElement(ElementName = "MTXPLUSSTR")]
        public string MTXPLUSSTR { get; set; }


        [XmlElement(ElementName = "MYRESULT")]
        public string MYRESULT { get; set; }


        [XmlElement(ElementName = "NRFAILED")]
        public string NRFAILED { get; set; }


        [XmlElement(ElementName = "NRPASSED")]
        public string NRPASSED { get; set; }


        [XmlElement(ElementName = "PRODUCTID")]
        public string PRODUCTID { get; set; }


        [XmlElement(ElementName = "PROGRAMFILE")]
        public string PROGRAMFILE { get; set; }


        [XmlElement(ElementName = "REMARKNAME")]
        public string REMARKNAME { get; set; }


        [XmlElement(ElementName = "REMARKVALUE")]
        public string REMARKVALUE { get; set; }

        public int IdSerijskiBrojBojler { get; set; }

        [XmlElement(ElementName = "SERIALNR")]
        [NotMapped] //This property will not be in SQL database table
        public string SERIALNR { get; set; }


        [XmlElement(ElementName = "STARTDATE")]
        public string STARTDATE { get; set; }


        [XmlElement(ElementName = "STATIONID")]
        public string STATIONID { get; set; }


        [XmlElement(ElementName = "STEPNR")]
        public string STEPNR { get; set; }


        [XmlElement(ElementName = "STEPTITLE")]
        public string STEPTITLE { get; set; }


        [XmlElement(ElementName = "TOTALRESULT")]
        public string TOTALRESULT { get; set; }


        [XmlElement(ElementName = "USERTESTER")]
        public string USERTESTER { get; set; }


        [XmlElement(ElementName = "WEEKNR")]
        public string WEEKNR { get; set; }

        public Sif_Serijski_Broj_Bojler Sif_Serijski_Broj_Bojler { get; set; }
        public STEP_CR STEP_CR { get; set; }
        public STEP_PW STEP_PW { get; set; }
        public STEP_H5 STEP_H5 { get; set; }
        public STEP_I5 STEP_I5 { get; set; }
        public STEP_FK STEP_FK { get; set; }
        public STEP_L1 STEP_L1 { get; set; }
        public STEP_CT STEP_CT { get; set; }

        public int? IdSTEP_CR { get; set; }
        public int? IdSTEP_PW { get; set; }
        public int? IdSTEP_H5 { get; set; }
        public int? IdSTEP_I5 { get; set; }
        public int? IdSTEP_FK { get; set; }
        public int? IdSTEP_L1 { get; set; }
        public int? IdSTEP_CT { get; set; }
        public int? IdAgr_ExtaStepStack { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
