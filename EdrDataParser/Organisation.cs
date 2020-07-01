namespace EdrDataParser
{
    class Organisation
    {
        public Organisation(string edrpouCode, string officialName, string address, string mainPerson, string occupation, string status)
        {
            EdrpouCode = edrpouCode;
            OfficialName = officialName;
            Address = address;
            MainPerson = mainPerson;
            Occupation = occupation;
            Status = status;
        }
        private string EdrpouCode { get; set; }
        private string OfficialName { get; set; }
        private string Address { get; set; }
        private string MainPerson { get; set; }
        private string Occupation { get; set; }
        private string Status { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
