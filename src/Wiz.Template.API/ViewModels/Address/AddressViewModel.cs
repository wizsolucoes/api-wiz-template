namespace Wiz.Template.API.ViewModels.Address
{
    public class AddressViewModel
    {
        public AddressViewModel(int id, string cep, string street, string streetFull, string uf)
        {
            Id = id;
            CEP = cep;
            Street = street;
            StreetFull = streetFull;
            UF = uf;
        }

        public int Id { get; set; }
        public string CEP { get; set; }
        public string Street { get; set; }
        public string StreetFull { get; set; }
        public string UF { get; set; }
    }
}
