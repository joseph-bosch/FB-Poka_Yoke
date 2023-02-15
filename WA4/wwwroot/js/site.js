// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


const otherCheckbox = document.querySelector('#chkbox');
//var mode = otherCheckbox.checked;
data2 = "false";
newCount = 0;
changedId = "";


const count = 1;
setInterval('refresh()', 1000);
setInterval('clear()', 10000);

function reply_clicked() {
    //alert(clicked_id);
    //changedId = clicked_id;
    $('#bodlist').load('SelectedPN' + "#bodlist");
    alert("here");

}


function AddManual() {
    //alert(clicked_id);
    //changedId = clicked_id;
    manual_Input = document.getElementById('addNum').value;
    var model = new Object();
    model.X3 = manual_Input

    $.ajax({
        type: "POST",
        url: "AddManual",
        dataType: "json",

        data: { data1: model },
        success: function (data) {
            alert(data);
        },
        fail: function (errMsg) {
            alert(errMsg);
        }
    });
    alert(manual_Input);
    $.ajax({
        url: "AddManual",
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            // process the data coming back
            document.getElementById('topUpdate').innerHTML = data;
            alert(data);

        },
    });
    //refresh();

}


function refresh() {
    //alert("here");
    //window.location.reload();

    $.ajax({
        url: "GetCurrent",
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            // process the data coming back
            document.getElementById('topUpdate').innerHTML = data;
            //alert(data);
            
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //alert(xhr.status);
            //alert(thrownError);
            //alert(item)
        }
    });
    
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

function clear() {
    const ManualInput = document.getElementById('addNum');

    // 👇️ clear input field
    ManualInput.value = '';
}



const btn = document.getElementById('AddBtn');

btn.addEventListener('keyup', function handleClick(event) {
    // 👇️ if you are submitting a form (prevents page reload)
    //event.preventDefault();

    const ManualInput = document.getElementById('addNum');

    // 👇️ clear input field
    ManualInput.value = '';
});

