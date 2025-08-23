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

 */



