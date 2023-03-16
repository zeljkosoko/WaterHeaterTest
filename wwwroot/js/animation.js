$(document).ready(function () {
    // animation with "odjava korisnika"
    $('nav .user').click(function () {
        $('.header-logout').slideToggle(400, 'swing');
    });

    // animation with "opcije sa strane"
    $('.sredina .sredina-sirina .opcije').click(function () {
        $('.opcije-otvorene').slideToggle(400, 'swing');
    });
    $('.sredina .sredina-sirina .opcije-otvorene .izlaz img').click(function () {
        $('.opcije-otvorene').slideToggle(400, 'swing');
    });
});
