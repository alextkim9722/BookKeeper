import { useState } from "react";

export function JsonRender(props)
{
    return(
        <>
        { props.success ? props.content : props.error }
        </>
    )
}