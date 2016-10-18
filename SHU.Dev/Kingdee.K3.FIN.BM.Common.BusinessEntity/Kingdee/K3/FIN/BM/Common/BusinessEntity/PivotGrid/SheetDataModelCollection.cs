namespace Kingdee.K3.FIN.BM.Common.BusinessEntity.PivotGrid
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [Serializable]
    public class SheetDataModelCollection : IEnumerable<SheetDataModel>, IEnumerable, INotifyCollectionChanged
    {
        private List<SheetDataModel> _sheets = new List<SheetDataModel>();

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public void Add(SheetDataModel sheet)
        {
            this._sheets.Add(sheet);
            this.OnCollectionChanged(NotifyCollectionChangedAction.Add, sheet, null);
        }

        public void Add(IList<SheetDataModel> sheets)
        {
            foreach (SheetDataModel model in sheets)
            {
                this.Add(model);
            }
        }

        public void Clear()
        {
            foreach (SheetDataModel model in this._sheets)
            {
                this._sheets.Remove(model);
                this.OnCollectionChanged(NotifyCollectionChangedAction.Remove, null, model);
            }
        }

        public IEnumerator<SheetDataModel> GetEnumerator()
        {
            foreach (SheetDataModel iteratorVariable0 in this._sheets)
            {
                yield return iteratorVariable0;
            }
        }

        public void Insert(int index, SheetDataModel sheet)
        {
            this._sheets.Insert(index, sheet);
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction changedAction, SheetDataModel newSheet, SheetDataModel oldSheet)
        {
            if (this.CollectionChanged != null)
            {
                NotifyCollectionChangedEventArgs e = null;
                switch (changedAction)
                {
                    case NotifyCollectionChangedAction.Add:
                        e = new NotifyCollectionChangedEventArgs(changedAction, newSheet);
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        e = new NotifyCollectionChangedEventArgs(changedAction, oldSheet);
                        break;

                    case NotifyCollectionChangedAction.Replace:
                        e = new NotifyCollectionChangedEventArgs(changedAction, newSheet, oldSheet);
                        break;
                }
                if (e != null)
                {
                    this.CollectionChanged(this, e);
                }
            }
        }

        public void Remove(SheetDataModel sheet)
        {
            this._sheets.Remove(sheet);
            this.OnCollectionChanged(NotifyCollectionChangedAction.Remove, null, sheet);
        }

        public void Remove(Guid sheetId)
        {
            SheetDataModel item = this._sheets.FirstOrDefault<SheetDataModel>(p => p.SheetId.Equals(sheetId));
            if (item != null)
            {
                this._sheets.Remove(item);
                this.OnCollectionChanged(NotifyCollectionChangedAction.Remove, null, item);
            }
        }

        public void Remove(int index)
        {
            SheetDataModel item = this._sheets.FirstOrDefault<SheetDataModel>(p => p.Index.Equals(index));
            if (item != null)
            {
                this._sheets.Remove(item);
                this.OnCollectionChanged(NotifyCollectionChangedAction.Remove, null, item);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (SheetDataModel iteratorVariable0 in this._sheets)
            {
                yield return iteratorVariable0;
            }
        }

        public int Count
        {
            get
            {
                return this._sheets.Count;
            }
        }

        public SheetDataModel this[int index]
        {
            get
            {
                return this._sheets.SingleOrDefault<SheetDataModel>(s => (s.Index == index));
            }
        }

        public SheetDataModel this[Guid sheetId]
        {
            get
            {
                SheetDataModel model = this._sheets.FirstOrDefault<SheetDataModel>(p => p.SheetId.Equals(sheetId));
                if (model == null)
                {
                    throw new NullReferenceException();
                }
                return model;
            }
        }


    }
}

