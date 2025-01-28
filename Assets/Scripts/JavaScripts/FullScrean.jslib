
var LibraryPageTool = {
    FullScrean: function()
    {
        var viewFullScreen = document.getElementById('#unity-canvas');
 
        var ActivateFullscreen = function()
        {
          
           
                viewFullScreen.webkitRequestFullscreen();
                console.log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaah");
        
         
 
           
        }
 
       
    }
};
mergeInto(LibraryManager.library, LibraryPageTool);