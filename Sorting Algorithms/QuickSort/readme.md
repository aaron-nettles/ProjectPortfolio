I first partition the array around a pivot element, then recursively sort the sub-arrays on the left and right of the pivot element. 

The partition function uses a pivot element, which is selected as the last element in the sub-array, to divide the sub-array into two parts.

 The elements smaller than or equal to the pivot are moved to the left side of the pivot, while the elements larger than the pivot are moved to the right side of the pivot. 
 
 The index of the pivot element is returned, and the QuickSort function uses this index to determine the sub-arrays to be sorted in the next recursion.