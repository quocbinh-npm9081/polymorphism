#region 104. A need for polymorphism (Nhu cầu đa hình)
/*
 104. A need for polymorphism (Nhu cầu đa hình)

    (1) Why do we sometimes need to manipulate different types in an uniform way

        Ý nghĩa:
        Câu hỏi này hỏi tại sao trong lập trình, đôi khi chúng ta cần thao tác với các kiểu 
        dữ liệu khác nhau theo một cách thống nhất (uniform way).

    (2) What polymorphism is
        
        Ý nghĩa:
        Đa hình là gì ?
 */


/* 
Giả sử chúng ta tạo 1 ứng dụng dùng để đặt pizza, người dùng có thể chọn 1 trong các loại pizza
được liệt kê và thành phần của mỗi chiếc pizza sẽ được hiển thị trên màn hình, ngoài ra, khi người
dùng di chuột qua 1 loại thành phần, các chi tiết như thời gian ủ của phô mai cheddar sẽ được hiển thị
(tức là người dùng di chuột vào bánh pizza cheddar)

Và đương nhiêu chúng ta sẽ thiết kế ứng dụng này theo hướng đối tượng nên với mỗi loại pizza chúng ta
sẽ xác định các lớp cho từng thành phần pizza 
 

Pizaa với thành phần chính là mô phai vàng (cheese)
    public class Cheddar
    {
        public string Name => "Cheddar cheese";
        public int AgedForMonths { get; }         // Thời gian ủ của phô mai
    }

Pizza với thành phần chính là nước sốt cà chua (Tomato sauce)
    public class TomatoSauce
    {
        public string Name => "Tomato sauce";
        public int TomatosIn100Grams { get; } // Lượng cà chua có trong 100grams bánh
    }

Pizaa với thành phần chính là phô mai trắng (Mozzarella)
    public class Mozzarella
    {
        public string Name => "Mozzarella";
        public bool IsLight { get; } //xác định xem loại phô mai Mozzarella này có phải là loại "light" (ít béo, ít calo) hay không.
    }

Bây giờ chúng ta sẽ tạo 1 class cho Pizza
    - lớp này sẽ chứa 1 danh sách các thành phần mà phiên bản pizza đó bao gồm
    - lớp này sẽ chứa phương thức để thêm thành phần vào Pizaa
    - lớp này sẽ chứa phương thức mổ tả để liệt kê tất cả thành phần
    
    public class Pizza
    {
        private List<???> _ingredients = new List<???>();
        public void AddIngredient(??? ingreduents) => _ingredients.Add(ingreduents);
        public string Describe() => $"This is a pizza with {string.Join(", ", _ingredients)}";
    }

Bâu giờ, vấn đề ở đây là chính xác thì danh sách thành phần lưu trữ loại đối tượng nào (kiểu) gì

:)) tốt nhất tôi nghỉ là làm sao để nó có thể lưu trữ được nhiều đối tượng thành phần pizza khác nhau :))
Nhưng điều này là không thể, vì 1 dánh sách chỉ có thể lưu trữ được một loại thành phần pizza mà thôi :v
và không chỉ danh sách đó mà chúng ta còn gặp vấn đề tương tự ở phương thức AddIngredient(), tham số hình 
thức, tham số truyền vào nên là loại thành phần gì ?

Tôi nghĩ ngay đến giải pháp Method Overloading (quá tải phương thức)

    public class Pizza
    {
        private List<???> _ingredients = new List<???>();
        public void AddIngredient(Cheddar cheddar) => _ingredients.Add(cheddar);
        public void AddIngredient(TomatoSauce tomatoSauce) => _ingredients.Add(tomatoSauce);
        public void AddIngredient(Mozzarella mozzarella) => _ingredients.Add(mozzarella);
        public string Describe() => $"This is a pizza with {string.Join(", ", _ingredients)}";
    }

:)) Tôi thật sự nghĩ rằng sử dụng Overrloading sẽ ổn nhưng nó vẫn không phải quyết được việc lưu trữ danh sách
các thành phần có trong pizza
mà nếu sử dụng overrloading thì nếu có 1000 thành phần, tôi phải khai báo 1000 cái method AddIngredient :))
Chết tiệt như thế là toang, nó sẽ gây ra nhiều vấn đề hơn nữa, chúng ta sẽ có thêm hang tá cá phương thức, tương
ứng với 1 loại thành phần và thực hiện cùng 1 công việc, chỉ khác nhau về loại tham số, điều này sẽ trông khủng 
khiếp và không thể duy trì được

!!! Tôi nhận ra rằng chúng ta nên có 1 đối chung cho các loại thành phần, nghĩa là với mỗi loại thành phần phát
sinh, chúng đều thuộc chung 1 đối tượng thành phần, 
Tóm lại là thành phần cheddar hay tomato hay mozzarella đều là => THÀNH PHẦN

và lớp Ingredient được tạo ra
public class Ingredient
{
    public string Name { get; set; }
    public string AgedForMonths { get; }
    public int TomatosIn100Grams { get; }
    public bool IsLight { get; }
}

// Bây giờ lại phát sinh vấn đề
là nếu chúng ta tạo 1 đối tượng là Cheddar thì người dùng lại thắc mắc rằng loại pizza cheddar sao lại có
cà chua trong 100 grams ....

:)) hmmm... Chúng ta cần 1 giải pháp khác
Chúng ta vẫn sẽ phải giữ nguyên từng loại khác nhau cho từng nguyên liệu, nhưng có thê xử lý chúng 1 các thống
nhất
Chúng ta vẫn sẽ có 1 danh sách các đối tượng Thành phần, nhưng với mỗi đối tượng có thể là 1 loại thành phần
khác nhau

==> Và đây là lúc tính ĐA HÌNH phát huy tác dụng
Đa hình cung cấp 1 gia diện 1 tập hợp duy nhất cho các thực thê thuộc các loại khác nhau


🌟 Minh họa đời sống
Giả sử bạn nói từ “CẮT”:
- Bác sĩ sẽ nghĩ đến “rạch dao mổ”
- Thợ tóc sẽ nghĩ đến “cắt tóc”
- Diễn viên sẽ nghĩ đến “ngừng diễn cảnh”
👉 Cùng một hành động, nhưng mỗi người hiểu và thực hiện khác nhau. Đó chính là đa hình.

 */
#endregion

#region 105. Inheritance
/* 
 105. Inheritance

    (1) What is inheritance is ?

        Ý nghĩa:
        Kết thừa là gì ?
    
    (2) What kind of relationship it creates between types 

        Ý nghĩa:
        Hỏi về mối quan hệ mà tính kế thừa (inheritance) tạo ra giữa các kiểu dữ liệu (class) trong lập trình
        hướng đối tượng.

    (3) What base classes and derived classes are
    
        Ý nghĩa:
        Lớp cơ sở, lớp dẫn xuất là gì ?

 */

/*
    Chúng ta sẽ tạo 1 lớp gọi là Ingredient (thành phần) và cho các lớp Cheddar, TomatoSauce, Mozzarella kết
    thừa lớp Ingredient

    Khi nào 3 loại dữ liệu Cheddar, TomatoSauce, Mozzarella khác nhau sẽ được xử lý thống nhất theo Ingredient

*/

//var cheddar = new Pizza();
//cheddar.AddIngredient(new Cheddar());
//cheddar.AddIngredient(new TomatoSauce());
//cheddar.AddIngredient(new Mozzarella());

///* 
//   Chúng ta đã tạo 1 chiếc Pizza Cheddar nhưng thành phần của nó lại có Cà chua ? có vẻ không ổn nhỉ
//   Sau này chúng ta sẽ tìm cách ngăn chặn việc khởi tạo các kiểu trừu tượng như vậy
//*/

//Console.WriteLine(cheddar.Describe());

//var cheddarPublicMethod = new Cheddar();
//Console.WriteLine(cheddarPublicMethod.PublicMethod()); // Mặc dù lớp Cheddar không có triển khai phương thức
//                                                       // PublicMethod(), nhưng khi khởi tạo đối tượng từ
//                                                       // class Cheddar chúng ta vãn có thể thực thi phương
//                                                       // thức PublicMethod(), vì PublicMethod() đã được 
//                                                       // class Cheddar kế thừa từ base class


//Console.ReadLine();

//public class Ingredient
//{
//    // Basic implementaion <- Basic class sẽ chứa các cách triển khai cơ bản mà bắt buộc các drived class phải tuân theo
//    public string PublicMethod() => "This method is PUBLIC in the Ingredient class.";
//}

//public class Cheddar : Ingredient
//{
//    public string Name => "Cheddar cheese";
//    public int AgedForMonths { get; } // Specific Implementaion  <- Mỗi kiểu dẫn  xuất (drived class) sẽ có
//                                      //                            các các triền khai (field, method) mà basic
//                                      //                            class và các drived class khác không có

//    // Chúng ta có thể thực thi phương thức PublicMethod của Base class(Ingredient) ngay bên trong class
//    public void UseMethodsFromBaseClass()
//    {
//        Console.WriteLine(PublicMethod());
//    }
//}

//public class TomatoSauce : Ingredient
//{
//    public string Name => "Tomato sauce";
//    public int TomatosIn100Grams { get; }

//}

//public class Mozzarella : Ingredient
//{
//    public string Name => "Mozzarella";
//    public bool IsLight { get; }

//}

//public class Pizza
//{
//    private List<Ingredient> _ingredients = new List<Ingredient> { };
//    public void AddIngredient(Ingredient ingredient) => _ingredients.Add(ingredient);
//    public string Describe() => $"This is a pizza with {string.Join(", ", _ingredients)}";
//}

#endregion

#region 107. Overriding members from the base class. Virtual methods and properties
/* 
 107. Overriding members from the base class. Virtual methods and properties

    (1) How to override the implementation of the method or a property from a base class in the drived class

        Ý nghĩa:
        Làm thế nào để ghi đè phương thức or thuộc tính của base class ngay bên trong drived class
    
    (2) What virtual method and properties are

        Ý nghĩa:
        Phương thức ảo và thuộc tính là gì

    (3) What method hiding is
    
        Ý nghĩa:
        Phương thức ẩn là gì 

 */

//Cheddar cheddar = new Cheddar();
//Ingredient ingredient = new Cheddar(); // GeneralType variable  = new SpecificType();

//Console.WriteLine(ingredient.Name);
//Console.ReadLine();
/*
 Khi chúng ta có 1 biến được khai báo 1 kiểu cụ thể và chúng ta gọi một số phương thức hoặc thuộc tính trên
 đó, công cụ bên trong cúa C# sẽ kiểu tra xem:
    1. Method đó có phải là Virtual hay không ?
        Nếu không, nó chỉ đơn giải là gọi phương thức của kiểu dữ liệu của biến được khao báo
        Nếu là virtual, nó sẽ kiểm tra ở Specific class có override không, nếu có thì thực thi phương thước
                        đó
                        nếu nó không không được override ở specific class thì nó sẽ thực thi phương thức virtual
 
*/
//public class Ingredient
//{
//    public virtual string Name { get; } = "Some ingredient"; // Kính thưa C# , khi bạn gọi phương thức này với
//                                                             // 1 đối tượng có GenaricType là Ingredient
//                                                             // làm ơn hãy kiểm tra xem với SpecificType có
//                                                             // menthod nào đang override không, nếu có hãy
//                                                             // thực thi phường thức override nếu không
//                                                             // hãy thực thi method vitrual.
//    public string PublicMethod() => "This method is PUBLIC in the Ingredient class.";
//}

//public class Cheddar : Ingredient
//{
//    public override string Name => "Cheddar cheese"; // Thực hiện ghi đè lên phương thức của Base class

//    public int AgedForMonths { get; }
//}

//public class TomatoSauce : Ingredient
//{
//    public string Name => "Tomato sauce";
//    public int TomatosIn100Grams { get; }

//}

//public class Mozzarella : Ingredient
//{
//    public string Name => "Mozzarella";
//    public bool IsLight { get; }

//}

//public class Pizza
//{
//    private List<Ingredient> _ingredients = new List<Ingredient> { };
//    public void AddIngredient(Ingredient ingredient) => _ingredients.Add(ingredient);
//    public string Describe() => $"This is a pizza with {string.Join(", ", _ingredients)}";
//}
#endregion

#region 119. Abstract classes
/* 
119. Abstract classes

    (1) How to prevent a class from being instantiated

        Ý nghĩa:
        Làm thế nào để ngăn chặn 1 lớp không có nó khởi tạo        

    (2) What abtract classes are

        Ý nghĩa:
        Lớp trừu tượng là gì ?

 */

/*
 Hiện tại chúng ta có 1 lớp là Ingredient, lớp này 1 KHÁI NIỆM trừu tượng được cụ thể hóa bới các lớp con của
 nó, vì vậy chúng ta không thể để bất kì kẻ kém cỏi nào instantiated lớp này, chúng ta có thể năng chạn điều đó 
 bằng các khiến lớp này trẻ nên trừu tượng

 Abstract class không thể được instantiated (khởi tạo ) chỉ đóng vai trò là lớp cơ sở cho các loại khác, 
 Nếu là Abstract method thì bắt buộc các drived class inherit abstract class phải implement method abstract.
 Abstract method luôn là Vitrual , vì nó không có body nên bắt buộc các drived class phải implement method đó
 */

//var ingredient = new Cheddar(); // cannot be instantiated - không thể khởi tạo 1 đối tượng từ 1 abstract class

//public abstract class Ingredient
//{
//    public virtual string Name { get; } = "Some ingredient";
//    public abstract void Prepare(); // Vì abstract class sẽ không bảo giờ được instantiated nên với method
//                                    // abstract không cần phải có body, khi các drived class 

//    public void Prepare1() // Không phải abtract nên drived class không cần phải implement
//    {
//        Console.WriteLine("á");
//    }
//}

//public class Cheddar : Ingredient
//{
//    public override string Name => "Cheddar cheese";

//    public int AgedForMonths { get; }

//    public override void Prepare() // implement abstract method của base class (Abstract)
//    {
//        throw new NotImplementedException();
//    }
//}

//public class TomatoSauce : Ingredient
//{
//    public string Name => "Tomato sauce";
//    public int TomatosIn100Grams { get; }

//    public override void Prepare()
//    {
//        throw new NotImplementedException();
//    }
//}

//public class Mozzarella : Ingredient
//{
//    public string Name => "Mozzarella";
//    public bool IsLight { get; }

//    public override void Prepare()
//    {
//        throw new NotImplementedException();
//    }
//}

//public class Pizza
//{
//    private List<Ingredient> _ingredients = new List<Ingredient> { };
//    public void AddIngredient(Ingredient ingredient) => _ingredients.Add(ingredient);
//    public string Describe() => $"This is a pizza with {string.Join(", ", _ingredients)}";
//}

#endregion

#region 125. A need for interfaces
/* 
125. A need for interfaces

    (1) Under what circumstances using an abstract class as a base type is not a good idea

        Ý nghĩa:
        Trong những trường hợp nào việc sử dụng lớp trừu tượng làm kiểu cơ sở là không 
        phải là một ý tưởng hay

    (2) Why we need interfaces

        Ý nghĩa:
        Tại sao chúng ta cần giao diện

 */

/*
 Quay trở lại ví dụ Pizza, giả sử nhà hàng muốn gia hạn ưu đãi và bán nhiều món Ý hơn không chỉ pizza
 Ví dụ chúng ta muốn cung cấp panettone, một loại bánh mì ngọt Ý truyền thống được ăn vào dịp Giáng sinh.

 */

/*
 Và giả sử các món ăn không chỉ phục vụ cho khác hàng mà còn dành cho nhân viên của nhà hàng
 khi nhân viên đăng nhập, ứng dụng sẽ hiển thị công thức chi tiết cho các món ăn được cung cấp.
 Ví dụ chúng ta muốn hiển thị hướng dẫn về thời gian và nhiệt độ nên nướng bánh pizza và panettone
*/
var bakeableDishes = new List<???>();
foreach (var bakeableDish in bakeableDishes)
{
    /*
        Vòng lặp này sẽ in ra màn hình nhân viên về công thức của món ăn
        nhưng vất đề ở đây là danh sách bakeableDishes nên là kiểu gì đây
        vì hiện tại Pizza và Panettone là 2 kiểu trừu tựng khác khau
        lúc này tôi mới nhận ra rằng chúng ta sẽ tạo 1 class abtract sẽ là trừu tượng cho 2 class
        Pizza và Panettone gọi là class Bakeable
    */
    Console.WriteLine(bakeableDish.GetInstructions());
}

/*
  Chúng ta có thể để Pizza inherit Bakeable nhưng hiện tại Panettone đang inherit Dessert mất r
 */
public abstract class Bakeable
{
    public abstract string GetInstructions();
}

// Tôi sẽ tạo class cho cái bánh panettone , và nó sẽ thuộc khái niệm lớp trừu trượng là 1 món tráng miệng
public abstract class Dessert
{
    new Pizza(),
    new Panettone()
}


public class Panettone : Dessert
{
    public string Name { get; set; }
}

public abstract class Ingredient
{
    public virtual string Name { get; } = "Some ingredient";
    public abstract void Prepare(); // Vì abstract class sẽ không bảo giờ được instantiated nên với method
                                    // abstract không cần phải có body, khi các drived class 

    public void Prepare1() // Không phải abtract nên drived class không cần phải implement
    {
        Console.WriteLine("á");
    }
}

public class Cheddar : Ingredient
{
    public override string Name => "Cheddar cheese";

    public int AgedForMonths { get; }

    public override void Prepare() // implement abstract method của base class (Abstract)
    {
        throw new NotImplementedException();
    }
}

public class TomatoSauce : Ingredient
{
    public string Name => "Tomato sauce";
    public int TomatosIn100Grams { get; }

    public override void Prepare()
    {
        throw new NotImplementedException();
    }
}

public class Mozzarella : Ingredient
{
    public string Name => "Mozzarella";
    public bool IsLight { get; }

    public override void Prepare()
    {
        throw new NotImplementedException();
    }
}

public class Pizza
{
    private List<Ingredient> _ingredients = new List<Ingredient> { };
    public void AddIngredient(Ingredient ingredient) => _ingredients.Add(ingredient);
    public string Describe() => $"This is a pizza with {string.Join(", ", _ingredients)}";
}

#endregion