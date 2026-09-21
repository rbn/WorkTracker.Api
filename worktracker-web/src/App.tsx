import { useEffect, useState } from 'react'

type WorkItem = {
  id: number
  title: string
  description: string
  isComplete: boolean
  createdAt: string
}

function App() {
  const [workItems, setWorkItems] = useState<WorkItem[]>([])

  useEffect(() => {
    fetch('https://localhost:7055/api/workitems')
      .then(response => response.json())
      .then(data => setWorkItems(data))
  }, [])

  return (
    <div>
      <h1>Work Tracker</h1>

      <ul>
        {workItems.map(item => (
          <li key={item.id}>{item.title}</li>
        ))}
      </ul>
    </div>
  )
}

export default App