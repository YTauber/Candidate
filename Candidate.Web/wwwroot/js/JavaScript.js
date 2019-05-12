$(() => {

    $("#confirm").on('click', function () {

        setStatus(1);
    })

    $("#decline").on('click', function () {

        setStatus(2);
    })

    function setStatus(status) {

        $(".btn").remove();

        $.post("/home/Update", { candidateid: $(".row").data('id'), status }, function () {

            $.get("/home/getcounts", function (counts) {

                setCounts(counts)
            })

        })
    }

    function setCounts(c) {

        $(".p").text(c.count.pendingCount);
        $(".c").text(c.count.confirmedCount);
        $(".d").text(c.count.declinedCount);
    }
    
    $("#toggle").on('click', function () {

        $(".notes").toggle();
    })
})