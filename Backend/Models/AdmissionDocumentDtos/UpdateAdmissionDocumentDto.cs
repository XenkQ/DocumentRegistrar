using System.ComponentModel.DataAnnotations;

namespace Backend.Models.AdmissionDocument;

internal class UpdateAdmissionDocumentDto
{
    public DateTime Date { get; set; }

    public string Symbol { get; set; }

    public int ContractorId { get; set; }
}
