
	$(document).ready(function(){
		// Activate tooltip
		$('[data-toggle="tooltip"]').tooltip();

	// Select/Deselect checkboxes
	var checkbox = $('table tbody input[type="checkbox"]');
	$("#selectAll").click(function(){
		if(this.checked){
		checkbox.each(function () {
			this.checked = true;
		});
		} else{
		checkbox.each(function () {
			this.checked = false;
		});
		}
	});
	checkbox.click(function(){
		if(!this.checked){
		$("#selectAll").prop("checked", false);
		}
	});
	});


//color

$(document).ready(function () {
	
	$(".hexcode").each(function (index, item) {
		$(this).css("background-color", $(this).text());
	})
	
});


//pagnation 

//$(document).ready(function () {
//	let page = window.location.search.substring(1).split("=")[1];
//	if (page == undefined) {
//		page = 1;
//	}

//	if (page == 1) {

//		$(".previous-link")?.addClass("disabled");
//	}
//	else {
//		$(".previous-link")?.removeClass("disabled");
//    }


//	$(".page-count")?.each(function (index, item) {
//		if ($(this).text() == page) {
//			$(this).parent('li').addClass("active");
//			$(this).addClass("disabled");
//		}
//		else {
//			$(this).parent('li').removeClass("active");
//			$(this).removeClass("disabled");
//		}

//	})

//	//$(".next-link").attr("asp-route-page", page + 1);
//	//$(".previous-link").attr("asp-route-page", page - 1);



	
//})

