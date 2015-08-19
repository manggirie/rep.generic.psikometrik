namespace web.sph.App_Code
{

    public class ProgramReportModel
    {
        private string m_ujian;
        public int Siri { get; set; }
        public int Tahun { get; set; }
        public int Bil { get; set; }

        public string Ujian
        {
            get
            {
                return m_ujian == "UKBP" ? "UKBP-A" : m_ujian;
            }
            set { m_ujian = value; }
        }

        public string Program { get; set; }
    }
}