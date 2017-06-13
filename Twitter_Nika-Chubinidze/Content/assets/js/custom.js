$(document).ready(function () {
    var isCover = false
    var isClickable = false
    var doubleFile = false

   /*============ Chat sidebar ========*/
  $('.chat-sidebar, .nav-controller, .chat-sidebar a').on('click', function(event) {
      $('.chat-sidebar').toggleClass('focus') 
  });

  $(".hide-chat").click(function(){
      $('.chat-sidebar').toggleClass('focus') 
  });

  /*============= About page ==============*/
  $(".about-tab-menu .list-group-item").click(function(e) {
      e.preventDefault() 
      $(this).siblings('a.active').removeClass("active") 
      $(this).addClass("active") 
      var index = $(this).index() 
      $("div.about-tab>div.about-tab-content").removeClass("active") 
      $("div.about-tab>div.about-tab-content").eq(index).addClass("active") 
  });

  /*==============  photos ===============*/
  $(".show-image").click(function(){
    var img = $(this).closest(".item-img-wrap").find("img:first").attr("src") 
    $("#showPhoto .modal-body").html("<img src='"+img+"' class='img-responsive'>") 
    $("#showPhoto").modal("show");
  })

  /*==============  show panel ===============*/
  $(".btn-frm").click(function(){
    $(".frm").toggleClass("hidden") 
    $(".frm").toggleClass("animated") 
    $(".frm").toggleClass("fadeInRight") 
  });

  /*==============  upload photo ===============*/
  $("img[id='profile']").click(function () {
      if (isClickable) {
          $("input[id='myFile']").click()
          isCover = false
          doubleFile = true
      } 
  });

  $("#cover").click(function () {
      if (isClickable && !doubleFile) {
          $("input[id='myCoverFile']").click()
          isCover = true
      } else {
          doubleFile = false
      }
  });

  /*==============  hide/show buttons ===============*/
  $("#btnedit").click(function () {
      isClickable = true 
      $("#btncancel,#btnsave").show() 
      $(this).hide() 
      $("#cover").wrap("<a href='#'></a>") 
      $("#cover-bottom").css("border", "5px solid lightgreen") 
      $("#profile").css("border","5px dotted lightgreen") 
  })

  $("#btncancel").click(function () {
      isClickable=false
      $("#cover-bottom").css("border", "")
      $("#cover").unwrap() 
      $("#profile").css("border", "")
      $("#btnedit").show() 
      $("#btncancel,#btnsave").hide() 
  })

 /*==============  save photo ===============*/
  $("#btnsave").click(function () {
      var file 

      if (!isCover) {
          file = $("#myFile")
          fileVal = file.val() 
      } else { 
          file = $("#myCoverFile") 
          fileVal = file.val()
      }

      var ext = fileVal.replace(/^.*\./, '').toLowerCase()

      restriction(fileVal,ext,file) 
  })
     
  function restriction(fileVal, ext,file) { 
      if (fileVal.length > 0) {
          if (file[0].files[0].size > 4048576) {
              alert("File must less than 4mb")
          }
          switch (ext) {
              case 'jpg':
              case 'jpeg':
              case 'png':
                  save()
                  break;
              default:
                  alert("Wrong format file") 
          }
      } else {
          alert("First choose photo")
      }
  }

  function save(){
      isCover ? document.forms['imgCoverSave'].submit() : document.forms['imgSave'].submit()
  }

  /*==============  make post ===============*/
  $("#txtPost").keyup(function () {
      var txt = $("#txtPost").val()
      if (txt.length > 0) {
          $("#btnButton").show()
      } else {
          $("#btnButton").hide()
      }
  })

  /*==============  search ===============*/
  $("#spanSearch").click(function () {
      if ($("#txtSearch").val().length > 0) {
          document.forms['searchForm'].submit()
      } 
  })
     
  $(".nonFriend").click(function () {
      alert("Follow the user to access timeline")
  })
     
})
 