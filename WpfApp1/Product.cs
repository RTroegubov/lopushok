
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------


namespace WpfApp1
{

using System;
    using System.Collections.Generic;
    
public partial class Product
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Product()
    {

        this.ProviderOrderProduct = new HashSet<ProviderOrderProduct>();

    }


    public int Id { get; set; }

    public string Title { get; set; }

    public int ProductTypeId { get; set; }

    public decimal Price { get; set; }

    public int StorageId { get; set; }

    public string Image { get; set; }



    public virtual ProductType ProductType { get; set; }

    public virtual Storage Storage { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<ProviderOrderProduct> ProviderOrderProduct { get; set; }

}

}
