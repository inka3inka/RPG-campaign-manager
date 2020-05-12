import React, { useState } from 'react';

export default function RightBar(props) {

  const clicker = () => {
    console.log("Hello")
  }

  return (
    <div className="navtile" onClick={clicker}>
      {props.name}
    </div>
  )
}