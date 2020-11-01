using Dynamo.Data;
using Dynamo.Model.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo.Repository.Common.AppSetting
{
    public interface IUISettingRepository
    {
    }
    public class UISettingRepository : IUISettingRepository
    {
        private readonly ApplicationDbContext _context;

        public UISettingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CssClass> GetCssClass(string className)
        {
            return await _context.CssClasses
                 .FirstOrDefaultAsync(m => m.Name == className);
        }
    }
}
