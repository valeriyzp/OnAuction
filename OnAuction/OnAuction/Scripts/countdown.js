var remaining = $("span.jb_timer").text(),
    regex = /\d{1,2}/g,
    matches = remaining.match(regex),
    hours = matches[0],
    minutes = matches[1],
    seconds = matches[2],
    remainingDate = new Date();

remainingDate.setHours(hours);
remainingDate.setMinutes(minutes);
remainingDate.setSeconds(seconds);

var intvl = setInterval(function () {
    var totalMs = remainingDate.getTime(),
        hours, minutes, seconds;
    
    remainingDate.setTime(totalMs - 1000);
    
    hours = remainingDate.getHours();
    minutes = remainingDate.getMinutes();
    seconds = remainingDate.getSeconds();
    
    if (hours === 0 && minutes === 0 && seconds === 0) {
        clearInterval(intvl);
    }
    
    $("span.jb_timer").text(
        (hours >= 10 ? hours : "0" + hours) + ":" +
        (minutes >= 10 ? minutes : "0" + minutes)  + ":" +
        (seconds >= 10 ? seconds : "0" + seconds));
    
}, 1000);
