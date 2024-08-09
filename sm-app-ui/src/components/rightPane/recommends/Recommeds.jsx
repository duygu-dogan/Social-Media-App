import React from 'react'
import './recommends.scss'

const Recommeds = () => {
    return (
        <>
            <div className='mx-8 my-6 py-4 border-solid border border-slate-600 rounded-xl'>
                <h2 className='text-xl ps-4 font-bold'>Who to Follow</h2>
                <div className='my-3'>
                    <div className='ps-4 my-1 pe-6 py-2 flex items-center justify-between recommended-user'>
                        <div className='flex items-center gap-3 '>
                            <img className="rounded-full h-10 w-10 hover:brightness-75" alt="recommended-user-pic" src='https://abs.twimg.com/sticky/default_profile_images/default_profile_bigger.png' />
                            <div className='flex flex-col'>
                                <strong>John Doe</strong>
                                <span className='text-sm text-gray-400'>@johndoe</span>
                            </div>
                        </div>

                        <button className='rounded-full bg-white text-black font-bold px-4 py-1 hover:bg-gray-200'>Follow</button>
                    </div>
                    <div className='ps-4 my-1 pe-6 py-2 flex items-center justify-between recommended-user'>
                        <div className='flex items-center gap-3 '>
                            <img className="rounded-full h-10 w-10 hover:brightness-75" alt="recommended-user-pic" src='https://abs.twimg.com/sticky/default_profile_images/default_profile_bigger.png' />
                            <div className='flex flex-col'>
                                <strong>John Doe</strong>
                                <span className='text-sm text-gray-400'>@johndoe</span>
                            </div>
                        </div>

                        <button className='rounded-full bg-white text-black font-bold px-4 py-1 hover:bg-gray-200'>Follow</button>
                    </div>
                    <div className='ps-4 my-1 pe-6 py-2 flex items-center justify-between recommended-user'>
                        <div className='flex items-center gap-3 '>
                            <img className="rounded-full h-10 w-10 hover:brightness-75" alt="recommended-user-pic" src='https://abs.twimg.com/sticky/default_profile_images/default_profile_bigger.png' />
                            <div className='flex flex-col'>
                                <strong>John Doe</strong>
                                <span className='text-sm text-gray-400'>@johndoe</span>
                            </div>
                        </div>

                        <button className='rounded-full bg-white text-black font-bold px-4 py-1 hover:bg-gray-200'>Follow</button>
                    </div>
                </div>
            </div>
        </>
    )
}

export default Recommeds