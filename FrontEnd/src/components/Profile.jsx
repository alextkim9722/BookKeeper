import "./Profile.css"

function Profile (props)
{
    return(
        <div id="profile-root">
            <div id="profile-div">
                <img id="profile-picture" src="/src/assets/default.png">
                </img>
            </div>
            <div>
                <h3>
                    {props.user.username}
                </h3>
            </div>
            <div>
                <h4>
                    {props.user.dateJoined}
                </h4>
            </div>
            <div>
                <span>Books Read</span>
                <span className="right-justified">{props.user.booksRead}</span>
            </div>
            <div>
                <span>Pages Read</span>
                <span className="right-justified">{props.user.pagesRead}</span>
            </div>
            <div>
                <p id="profile-description">{props.user.description}</p>
            </div>
        </div>
    )
}

export default Profile