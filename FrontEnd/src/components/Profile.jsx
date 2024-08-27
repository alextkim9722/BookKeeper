import "./Profile.css"

function Profile ()
{
    return(
        <div id="profile-root">
            <div id="profile-div">
                <img id="profile-picture" src="/src/assets/default.png">
                </img>
            </div>
            <div>
                <h3>
                    ALEXANDER
                </h3>
            </div>
            <div>
                <h4>
                    January 1st 2003
                </h4>
            </div>
            <div>
                <span>Books Read</span>
                <span className="right-justified">0</span>
            </div>
            <div>
                <span>Pages Read</span>
                <span className="right-justified">0</span>
            </div>
            <div>
                <p id="profile-description"></p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec.
            </div>
        </div>
    )
}

export default Profile