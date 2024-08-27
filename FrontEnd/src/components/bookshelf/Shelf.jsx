import { BookOnShelf } from "./BookOnShelf"
import "./Shelf.css"

function Shelf()
{
    return(
        <>
            <div>
                <img src="/src/assets/shelf-head.png"></img>
            </div>
            <div id="shelf-position">
                <BookOnShelf bookId='5'/>
                <BookOnShelf bookId='5'/>
                <BookOnShelf bookId='5'/>
                <BookOnShelf bookId='5'/>
                <BookOnShelf bookId='5'/>
            </div>
        </>
    )
}

export default Shelf