<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mayando.Web.Infrastructure.NavigationContext>" %>
                    <span class="navigation-slideshow">
                        <% if(Model.IsSlideshow) { %>
                        <a href="#" onclick="slideshowGoBackwards();return false;" title="<%= Resources.SlideshowGoBackwards %>" id="navigation-slideshow-backwards"><img src="<%= Url.ThemedContent("~/Content/backwards.png") %>" alt="<%= Resources.SlideshowGoBackwards %>" /></a>
                        <a href="#" onclick="slideshowPause();return false;" title="<%= Resources.SlideshowPause %>" id="navigation-slideshow-pause"><img src="<%= Url.ThemedContent("~/Content/pause.png") %>" alt="<%= Resources.SlideshowPause %>" /></a>
                        <a href="#" onclick="slideshowGoForwards();return false;" title="<%= Resources.SlideshowGoForwards %>" id="navigation-slideshow-forwards"><img src="<%= Url.ThemedContent("~/Content/forwards.png") %>" alt="<%= Resources.SlideshowGoForwards %>" /></a>
                        <a href="#" onclick="slideshowSpeedUp();return false;" title="<%= Resources.SlideshowSpeedUp %>" id="navigation-slideshow-speedup"><img src="<%= Url.ThemedContent("~/Content/speedup.png") %>" alt="<%= Resources.SlideshowSpeedUp %>" /></a>
                        <a href="#" onclick="slideshowSpeedDown();return false;" title="<%= Resources.SlideshowSpeedDown %>" id="navigation-slideshow-speeddown"><img src="<%= Url.ThemedContent("~/Content/speeddown.png") %>" alt="<%= Resources.SlideshowSpeedDown %>" /></a>
                        <% } else { %>
                        <a href="<%= Url.PhotoDetails(Model.Current, Model.Type, Model.Criteria, Model.DefaultSlideshowDelay) %>" title="<%= Resources.SlideshowStart %>" id="navigation-slideshow-start"><img src="<%= Url.ThemedContent("~/Content/start.png") %>" alt="<%= Resources.SlideshowStart %>" /></a>
                        <% } %>
                    </span>