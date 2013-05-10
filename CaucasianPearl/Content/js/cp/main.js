$(document).ready(function($) {
    var url = window.location;
    $('.menu a[href="' + url + '"]').addClass('current_page_item');
})