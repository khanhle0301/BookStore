var product = {
    init: function () {
        product.loadImages();       
        product.registerEvents();
    },
    registerEvents: function () {
        $('#btnChooImages').off('click').on('click', function (e) {
            e.preventDefault();
            var finder = new CKFinder();
            finder.selectActionFunction = function (url) {
                $('#imageList').append('<div style="float:left"><img src="' + url + '" width="50" /><a href="#" class="btn-delImage"><i class="fa fa-times"></i></a></div>');
                $('.btn-delImage').off('click').on('click', function (e) {
                    e.preventDefault();
                    $(this).parent().remove();
                    product.loadtxtMoreImage();
                });
            };
            finder.popup();
            product.loadtxtMoreImage();
        });
    },
    loadImages: function () {
        $.ajax({
            url: '/Admin/Product/LoadImages',
            type: 'GET',
            data: {
                id: $('#hidProductID').val()
            },
            dataType: 'json',
            success: function (response) {
                var data = response.data;
                var html = '';
                $.each(data, function (i, item) {
                    html += '<div style="float:left"><img src="' + item + '" width="50"/><a href="#" class="btn-delImage"><i class="fa fa-times"></i></a></div>'
                });
                $('#imageList').html(html);

                $('.btn-delImage').off('click').on('click', function (e) {
                    e.preventDefault();
                    $(this).parent().remove();
                });
                product.loadtxtMoreImage();
            }
        });
    },
    loadtxtMoreImage: function () {
        var images = [];
        $.each($('#imageList img'), function (i, item) {
            images.push($(item).prop('src'));
        })
        $('#txtmoreImage').val(JSON.stringify(images));
    }
}
product.init();