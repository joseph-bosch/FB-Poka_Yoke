// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


const otherCheckbox = document.querySelector('#chkbox');
//var mode = otherCheckbox.checked;
data2 = "false";
newCount = 0;
changedId = "";


const count = 1;

function reply_clicked() {
    //alert(clicked_id);
    //changedId = clicked_id;
    $('#bodlist').load('SelectedPN' + "#bodlist");
    alert("here");

}

function sendmsg() {
    var model = new Object();
    model.X1 = data2
    model.X2 = changedId.replace(/[_][0-9]/g, '');
    
    $.ajax({
        type: "POST",
        url: "SelectedPN",
        dataType: "json",
        
        data: { data1: model },
        success: function (data) {
            alert(data);
        },
        fail: function (errMsg) {
            alert(errMsg);
        }
    });
    
    alert(event.srcElement.id);
    
    
   
    
 
}

function increment() {
    let x = parseInt(document.getElementById('updateCount').innerHTML);
    let currVal = parseInt(document.getElementById('sumAll').innerHTML);
    newCount = parseInt(document.getElementById('sumAll').innerHTML) + 1;
    if (currVal < x) {
        document.getElementById('sumAll').innerHTML = newCount;
    }

    else {
    }
    
    sendmsg();
}

function decrement() {
    let x = document.getElementById('sumAll').innerHTML;
    newCount = parseInt(document.getElementById('sumAll').innerHTML) - 1;
    document.getElementById('sumAll').innerHTML = parseInt(x) - 1;
}


