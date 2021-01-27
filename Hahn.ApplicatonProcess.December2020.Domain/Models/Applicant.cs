using FluentValidation;

namespace Hahn.ApplicationProcess.Application.Hahn.ApplicatonProcess.December2020.Domain.Models
{
    public class Applicant
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public string CountryOfOrigin { get; set; }
        public string EmailAddress { get; set; }
        public int Age { get; set; }
        public bool Hired { get; set; }

    }

    public class ApplicantValidator : AbstractValidator<Applicant>
    {
        public ApplicantValidator()
        {
            RuleFor(x => x.Name).MinimumLength(5);
            RuleFor(x => x.FamilyName).MinimumLength(5);
            RuleFor(x => x.Address).MinimumLength(10);
            RuleFor(x => x.EmailAddress).EmailAddress();
            RuleFor(x => x.Age).InclusiveBetween(20,60);
            RuleFor(x => x.Hired).NotNull();
            //RuleFor(x => x.CountryOfOrigin)



           




        }
    }

}
