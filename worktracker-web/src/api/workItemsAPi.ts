type WorkItem = {
  id: number
  title: string
  description: string
  isComplete: boolean
  createdAt: string
}

const API_URL = 'https://localhost:7055/api/workitems'

export async function getWorkItems(): Promise<WorkItem[]> {
  const response = await fetch(API_URL)

  if (!response.ok) {
    throw new Error('Failed to load work items')
  }

  return response.json()
}