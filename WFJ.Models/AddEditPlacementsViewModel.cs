using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WFJ.Models
{
    public class AddEditPlacementsViewModel: ExceptionModel
    {
        public string ClientName { get; set; }
        public IEnumerable<FormSectionViewModel> FormSections { get; set; }

        // form fields
        public List<FormFieldViewModel> FormFieldsList { get; set; }

        public IEnumerable<SelectListItem> CurrencyDropdown { get; set; }


        public List<SelectListItem> Requestors { get; set; }
        public List<SelectListItem> AssignedAtorneys { get; set; }
        public List<SelectListItem> Collectors { get; set; }
        public List<SelectListItem> StatusList { get; set; }

        public RequestViewModel Request { get; set; }
    }



    public class FormSectionViewModel
    {
        public int formSectionID { get; set; }
        public string sectionName { get; set; }
        public Nullable<int> sequenceID { get; set; }
    }

    public class FormFieldViewModel
    {
        public int ID { get; set; }
        public Nullable<int> FormID { get; set; }
        public Nullable<int> FieldTypeID { get; set; }
        public Nullable<int> formSectionID { get; set; }
        public Nullable<byte> Required { get; set; }
        public Nullable<int> Length { get; set; }
        public string FieldName { get; set; }
        public Nullable<int> SeqNo { get; set; }
        public Nullable<int> ListSeqNo { get; set; }
        public Nullable<int> AccessLevel { get; set; }
        public Nullable<byte> EMailField { get; set; }
        public Nullable<int> SelectionColumn { get; set; }
        public string EPDBFieldName { get; set; }
        public string SQLStatement { get; set; }
        public Nullable<int> AccountSummarySeqNo { get; set; }
        public Nullable<byte> showOnCalendar { get; set; }
        public Nullable<int> rowNumber { get; set; }
        public FieldSizeViewModel FieldSize { get; set; }



        public FormDataViewModel FormData { get; set; }
        public FormAddressDataViewModel FormAddressData { get; set; }



        public IEnumerable<FormSelectionListViewModel> FormSelectionLists { get; set; }
    }

    public class FieldSizeViewModel
    {
        public int fieldSizeID { get; set; }
        public string fieldSize1 { get; set; }
        public string htmlCode { get; set; }
        public Nullable<int> seqNo { get; set; }
    }

    public class FormSelectionListViewModel
    {
        public int ID { get; set; }
        public int? FormFieldID { get; set; }
        public string Code { get; set; }
        public string TextValue { get; set; }
        public int? SeqNo { get; set; }
    }

    public class FormDataViewModel
    {
        public int ID { get; set; }
        public Nullable<int> RequestID { get; set; }
        public Nullable<int> FormFieldID { get; set; }
        public string FieldValue { get; set; }
        public Nullable<int> currencyID { get; set; }
    }


    public class FormAddressDataViewModel
    {
        public int ID { get; set; }
        public Nullable<int> RequestID { get; set; }
        public Nullable<int> FormFieldID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }

}
