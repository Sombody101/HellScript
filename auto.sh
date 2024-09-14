#!/bin/bash

###
# This script prints if-elseif-else chains that are tedious to make manually 
###

types=("HellBigInteger" "HellInteger" ) # "HellNumber" "HellBigNumber")

operator='+'
count=0

generate_if_else() {
    type1="$1"
    type2="$2"

    echo "else if (left is $type1 && right is $type2)"
    echo "{"
    echo "    // Handle $type1 $operator $type2"
    echo "    return ($type1)left + ($type2)right;"
    echo "}"

    (( count += 1 ))
}

for type1 in "${types[@]}"; do
    for type2 in "${types[@]}"; do
        # echo
        # echo "// Handler for types $type1 and $type2"
        generate_if_else "$type1" "$type2"
    done
done
echo "$count cases"