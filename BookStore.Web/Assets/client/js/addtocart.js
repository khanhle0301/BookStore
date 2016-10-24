var common = {
    init: function () {
        common.registerEvents();
    },
    registerEvents: function () {
        $('.btnAddToCart').off('click').on('click', function (e) {
            e.preventDefault();           
            var quantity = parseInt($('#quantity').val());
            $.ajax({
                url: '/ShoppingCart/Add',
                data: {
                    productId: $(this).data('id'),
                    quantity: quantity,                    
                },
                type: 'POST',
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        alert('Thêm sản phẩm thành công.');
                        document.location.reload();
                    }
                    else {
                        alert(response.message);
                    }
                }
            });
        });
    }
}
common.init();