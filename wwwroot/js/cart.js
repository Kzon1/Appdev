function addItem(bookId, price,  quantity = 1) {
    fetch(`/User/Cart/AddItem?bookId=${bookId}&quantity=${quantity}`, {
        method: 'GET',
    }).then((res) => {
        if (res.ok) {
            UpdateQuantity(bookId);
            TotalCount(bookId);
            
        }
    })
}
function inputQuantityValue(bookId, quantity) {
    fetch(`/User/Cart/InputQuantity?bookId=${bookId}&quantity=${quantity}`, {
        method: 'GET',
    }).then((res) => {
        if (res.ok) {
            UpdateQuantity(bookId);
            TotalCount(bookId);           
        }
    })
}
function removeItem(bookId, price, quantity = 1) {
    fetch(`/User/Cart/RemoveItem?bookId=${bookId}&quantity=${quantity}`, {
        method: 'GET',
    }).then((res) => {
        if (res.ok) {
            UpdateQuantity(bookId);
            TotalCount(bookId);          
        }
    })
}
function TotalCount(bookId) {
    var id = `Total-${bookId}`;
    $.ajax({
        url: "/User/Cart/GetTotalPrice?bookId=" + bookId,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $(`#${id}`).html(result);
        }
    });
}
function UpdateQuantity(id) {
    $.ajax({
        url: "/User/Cart/ItemQuantity?bookId=" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $(`#${id}`).val(result);
        }
    });
}
