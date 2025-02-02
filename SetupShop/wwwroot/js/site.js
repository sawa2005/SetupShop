﻿$(function () {
    if ($("a.confirmDeletion").length) {
        $("a.confirmDeletion").click(() => {
            if (!confirm("Confirm deletion")) return false;
        });
    }

    if ($("div.alert.notification").length) {
        setTimeout(() => {
            $("div.alert.notification").fadeOut();
        }, 2000);
    }
});

function readURL(input) {
    if (input.files && input.files[0]) {
        let reader = new FileReader();

        reader.onload = function (e) {
            $("img#imgpreview").attr("src", e.target.result).width(300);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

/* const elements = document.querySelectorAll('*');

elements.forEach(element => {
    element.removeAttribute(element.getAttributeNames().find(attr => attr.startsWith('b-')));
});