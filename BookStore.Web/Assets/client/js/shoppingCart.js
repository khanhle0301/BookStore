var cart = {
    init: function () {       
        cart.registerEvent();
    },
    registerEvent: function () {
        $('#checkout').off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = "/thanh-toan.html";
        });
        $('#btnUpdate').off('click').on('click', function () {
            var cartList = [];
            $.each($('.txtQuantity'), function (i, item) {
                cartList.push({
                    ProductId: $(item).data('id'),
                    Quantity: $(item).val()                   
                });
            });
            $.ajax({
                url: '/ShoppingCart/Update',
                type: 'POST',
                data: {
                    cartData: JSON.stringify(cartList)
                },
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        window.location.href = "/gio-hang.html";
                    }
                }
            });
        });

        $('.btnDeleteItem').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));          
            cart.deleteItem(productId);
        });
    },

    deleteItem: function (productId) {
        $.ajax({
            url: '/ShoppingCart/DeleteItem',
            data: {
                productId: productId               
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    window.location.href = "/gio-hang.html";
                }
            }
        });
    }
}
cart.init();