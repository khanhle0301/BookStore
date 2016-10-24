var contact = {
    init: function () {
        contact.registerEvent();
    },
    registerEvent: function () {

        $('#frmContact').validate({
            rules: {
                name: "required",
                email: {
                    required: true,
                    email: true
                },
                phone: {
                    required: true,
                    number: true
                },
                message: "required"
            },
            messages: {
                name: "Yêu cầu nhập tên",
                message: "Yêu cầu nhập nội dung",
                email: {
                    required: "Bạn cần nhập email",
                    email: "Định dạng email chưa đúng"
                },
                phone: {
                    required: "Số điện thoại được yêu cầu",
                    number: "Số điện thoại phải là số."
                }
            }
        });

        $('#send_contact').off('click').on('click', function (e) {
            e.preventDefault();
            var isValid = $('#frmContact').valid();
            if (isValid) {
                contact.createContact();
            }
        });

        //contact.initMap();
    },

    createContact: function () {
        var contact = {
            Name: $('#name_contact').val(),
            Email: $('#email_contact').val(),
            Phone: $('#telephone_contact').val(),
            Message: $('#message_contact').val(),
            Status: false
        }
        $.ajax({
            url: '/Page/SendFeedback',
            type: 'POST',
            dataType: 'json',
            data: {
                feebackViewModel: JSON.stringify(contact)
            },
            success: function (response) {

                if (response.status) {
                    if (response.urlCheckout != undefined && response.urlCheckout != '') {
                        window.location.href = response.urlCheckout;
                    }
                    else {
                        setTimeout(function () {
                            alert('Gởi phản hồi thành công');
                            $('#name_contact').val(''),
                            $('#email_contact').val(''),
                            $('#telephone_contact').val(''),
                            $('#message_contact').val('')
                        });
                    }

                }
                else {
                    $('#contactResult').show();
                    $('#contactResult').text(response.message);
                }
            }
        });
    }

    //initMap: function () {
    //    var uluru = { lat: parseFloat($('#hidLat').val()), lng: parseFloat($('#hidLng').val()) };
    //    var map = new google.maps.Map(document.getElementById('map'), {
    //        zoom: 17,
    //        center: uluru
    //    });

    //    var contentString = $('#hidAddress').val();

    //    var infowindow = new google.maps.InfoWindow({
    //        content: contentString
    //    });

    //    var marker = new google.maps.Marker({
    //        position: uluru,
    //        map: map,
    //        title: $('#hidName').val()
    //    });
    //    infowindow.open(map, marker);
    //}
}

contact.init();