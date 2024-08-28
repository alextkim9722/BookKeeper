import { Shelf } from './Shelf';
import { useEffect, useState } from 'react'
import './Bookshelf.css';

function Bookshelf(props)
{
    const [bookIds, setBookIds] = useState(props.bookIds)

    useEffect(() => {
        setBookIds(props.bookIds)
    }, [props.bookIds]);

    return(
        <>
            <div id="bookshelf-root">
                <Shelf bookIds={bookIds.slice(0, 5)}/>
            </div>
        </>
    )
}

export default Bookshelf