
    function myFunction() {

        $(document).on("click", "#btnDetail", function () {
            //var id =   $(this).attr("Id");
            var id = $(this).attr("productid");
            console.log(id);

            $.ajax(
                {
                    url: "/Product/ProductById",
                    data: { id: id },
                    method: "post",
                    datatype: "json",
                    success: function (response) {
                        if (response.Result) {

                            $("#ProductName").val(response.Product.ProductName);
                            $("#Cost").val(response.Product.Cost);
                            $("#Detail").val(response.Product.Detail);
                            $("#CategoryName").val(response.Product.CategoryName);
                            $("#ProductModal").modal("show");

                        }


                    },
                    error: function () {
                        alert("Bir Hata Oluştu")

                    }
                });






        })
    }


    $( document ).ready(function() {
        console.log("ready!");
});
