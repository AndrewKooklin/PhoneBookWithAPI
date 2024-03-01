namespace PhoneBookWPF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PhoneBookRecords
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string FathersName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }
    }
}
