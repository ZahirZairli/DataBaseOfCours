function update_amounts() {
    var sum = 0.0;
    $('#mytable > tbody > tr:not([style*="display: none"])').each(function () {
        var price = $(this).find('#price').text();
        sum += Number(price);
    })
    $('#total').text(sum);
}

$(document).ready(function () {
    update_amounts();
    $('select').on('change', function () {
        update_amounts();
    });
    $('#myInput').on('keyup', function () {
        update_amounts();
    });
});





//$('#ulza li').on('click', function () {
//    alert("salam");

//}); 











function myFunction() {
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("mytable");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[1];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

function myFunction1() {
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("myInput1");
    filter = input.value.toUpperCase();
    table = document.getElementById("mytable1");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[1];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

function myFunction2() {
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("myInput2");
    filter = input.value.toUpperCase();
    table = document.getElementById("mytable2");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[1];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}



function reng() {
    var element = document.getElementsByClassName("za");
    element.classList.add("mystyle");
}

//let ahref = document.querySelector('.za');
//ahref.addEventListener('click', () => ahref.style.color = 'red !important');
















//const openEls = document.querySelectorAll("[data-open]");
//const closeEls = document.querySelectorAll("[data-close]");
//const isVisible = "is-visible";

//for (const el of openEls) {
//    el.addEventListener("click", function () {
//        const modalId = this.dataset.open;
//        document.getElementById(modalId).classList.add(isVisible);
//    });
//}

//for (const el of closeEls) {
//    el.addEventListener("click", function () {
//        this.parentElement.parentElement.parentElement.classList.remove(isVisible);
//    });
//}

//document.addEventListener("click", e => {
//    if (e.target == document.querySelector(".modal.is-visible")) {
//        document.querySelector(".modal.is-visible").classList.remove(isVisible);
//    }
//});

//document.addEventListener("keyup", e => {
//    // if we press the ESC
//    if (e.key == "Escape" && document.querySelector(".modal.is-visible")) {
//        document.querySelector(".modal.is-visible").classList.remove(isVisible);
//    }
//});



function myFunctionSel() {
    var select, filter, table, tr, td, i, txtValue;
    select = document.getElementById("select_id");
    filter = select.value;
    table = document.getElementById("mytable");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[6];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}