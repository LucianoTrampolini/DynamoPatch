using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Dynamo.Model.Base;

namespace Dynamo.Model
{
    public class Oefenruimte : ModelBase
    {
        public Oefenruimte()
        {
            Tarieven = new List<Tarief>();
        }

        [MaxLength(50)]
        public string Naam { get; set; }

        public virtual ICollection<Tarief> Tarieven { get; set; }
    }
}