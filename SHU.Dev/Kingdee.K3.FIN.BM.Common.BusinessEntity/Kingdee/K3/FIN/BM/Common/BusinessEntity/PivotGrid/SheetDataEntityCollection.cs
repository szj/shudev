namespace Kingdee.K3.FIN.BM.Common.BusinessEntity.PivotGrid
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class SheetDataEntityCollection : IEnumerable<SheetDataEntity>, IEnumerable
    {
        private Dictionary<Guid, SheetDataEntity> _sheet = new Dictionary<Guid, SheetDataEntity>();

        public void Add(SheetDataEntity sheet)
        {
            this._sheet.Add(sheet.SheetId, sheet);
        }

        public IEnumerator<SheetDataEntity> GetEnumerator()
        {
            foreach (SheetDataEntity iteratorVariable0 in this._sheet.Values)
            {
                yield return iteratorVariable0;
            }
        }

        public void Remove(Guid sheetId)
        {
            this._sheet.Remove(sheetId);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (SheetDataEntity iteratorVariable0 in this._sheet.Values)
            {
                yield return iteratorVariable0;
            }
        }

        public SheetDataEntity this[Guid sheetId]
        {
            get
            {
                return this._sheet[sheetId];
            }
        }


    }
}

