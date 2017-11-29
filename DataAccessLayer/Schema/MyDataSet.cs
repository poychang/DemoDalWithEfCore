using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Schema
{
    /// <summary>資料表模型</summary>
    public class MyDataSet
    {
        /// <summary>識別碼</summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>說明</summary>
        public string Description { get; set; }
    }
}
