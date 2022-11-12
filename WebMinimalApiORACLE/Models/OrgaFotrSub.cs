using Microsoft.EntityFrameworkCore;

namespace WebMinimalApiORACLE.Models
{
    [Keyless]
    public class OrgaFotrSub
    {
        public int ORGA_CD_ORGAO { get; set; }
        public int FOTR_CD_FORCA_TRABALHO { get; set; }
        public DateTime ORFS_DT_INICIO_VIGENCIA { get; set; }
        public DateTime ORFS_DT_FINAL_VIGENCIA { get; set; }
    }
}
