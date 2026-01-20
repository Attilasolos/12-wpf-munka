using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szavazogep
{
    public class Candidate
    {
        public int District { get; set; }
        public int Votes { get; set; }
        public string Name { get; set; } = "";
        public string PartyShort { get; set; } = "";

        public string Party
        {
            get
            {
                switch (PartyShort)
                {
                    case "GYEP":
                        return "Gyümölcsevők Pártja";
                    case "HEP":
                        return "Húsevők Pártja";
                    case "ZEP":
                        return "Zöldségevők Pártja";
                    case "TISZ":
                        return "Tejivók Szövetsége";
                    default:
                        return "Független";
                }
            }
        }

        public Candidate(string row)
        {
            var data = row.Split(' ');
            this.District = int.Parse(data[0]);
            this.Votes = int.Parse(data[1]);
            this.Name = $"{data[2]} {data[3]}";
            this.PartyShort = data[4];
        }
    }
}
