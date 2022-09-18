$(function() {
    $("#tblDepartmanlar").on("click", ".btDepartmanSil", function () {
        if (confirm("Departmanı silmek istediğinize emin misiniz?")) {
            var id = $(this).data("ide");
            var btn = $(this);
            //alert(id);
            $.ajax({
                type: "GET",
                url: "/Departman/Sil/" + id,
                success: function() {
                    btn.parent().parent().remove();
                }
            });
        }

        
    });
    $("#tblCustomer").on("click",
        ".btnCustomer",
        function() {
            if (confirm("Customeri silmek istediğinize emin misiniz?")) {
                var id = $(this).data("id");
                var btn = $(this);
                $.ajax({
                    type: "GET",
                    url: "/Customer/Sil/" + id,
                    success: function() {
                        btn.parent().parent().remove();
                    }
                });
            }

        }
    );
    $("#tblSorun").on("click", ".btnsuccess",
        function () {
            alert("tıkla");
            var id = $(this).data("id");
            $.ajax({
                 type: "GET",
                url: "/Sorunlar/Degistir/" + id,
                success: function() {
                    alert("asd");
                }

                }
            );
        }
    );
});

function CheckDateTypeIsValid(dateElement) {

    var value = $(dateElement).val();
    if (value == '') {
        $(dateElement).valid("false");
    } else {
        $(dateElement).valid();
    }
}