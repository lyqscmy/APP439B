using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace APP439B
{
  public abstract class BindableObject : INotifyPropertyChanged
  {

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    protected void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
    {
      string propertyName = ExtractPropertyName<T>(propertyExpression);
      this.OnPropertyChanged(propertyName);
    }

    private string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
    {
      if (propertyExpression == null)
      {
        throw new ArgumentNullException("propertyExpression");
      }
      MemberExpression memberExpression = propertyExpression.Body as MemberExpression;
      if (memberExpression == null)
      {
        throw new ArgumentException("propertyExpression");
      }
      PropertyInfo propertyInfo = memberExpression.Member as PropertyInfo;
      if (propertyInfo == null)
      {
        throw new ArgumentException("propertyExpression");
      }
      MethodInfo getMethod = propertyInfo.GetMethod;
      if (getMethod.IsStatic)
      {
        throw new ArgumentException("propertyExpression");
      }
      return memberExpression.Member.Name;
    }
  }
}
