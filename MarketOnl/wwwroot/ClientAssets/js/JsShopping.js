

$(document).ready(function () {
    ShowCount();
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quatity = 1;
        var textSL = $('#quantity_value').text();
        if (textSL != '') {
            quatity = parseInt(textSL);
        }
       
           //alert(id + '' + quatity)


        $.ajax({

            url: '/shoppingcart/addtocart',
            type: 'POST',
            data: { id: id, quantity: quatity },
            success: function (rs) {
               /* console.log(rs);*/
                if (rs.success) {
                    $('#checkout_items').html(rs.count);
                    alert(rs.msg)
                } else {
                    alert("Có lỗi xảy ra khi thêm sản phẩm vào giỏ hàng.");
                }
            },
             error: function (xhr, status, error) {
                console.log(xhr);
                console.log(status);
                console.log(error);
            }
        });




    });




});

function LoadCart() {
    $.ajax({

        url: '/shoppingcart/Partial_View_ItemCart',
        type: 'GET',
        success: function (rs) {
            $('#load_data').html(rs);
        }

    })

}



function ShowCount() {
    $.ajax({

        url: '/shoppingcart/ShowCount',
        type: 'GET',
        success: function (rs) {
            $('#checkout_items').html(rs.count);
        }
 
    })

}

