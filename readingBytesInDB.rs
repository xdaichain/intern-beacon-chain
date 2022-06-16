use rocksdb::DB;

fn print_type_of<T>(_: &T) {
    println!("{}", std::any::type_name::<T>())
}

fn main() {

	let dbDirectory : String = "/Users/macbook/Desktop/nethermindLocalTestnet/private-networking/node_1/db/clique/".to_owned();

	let readingFolder = "blocks";

	let db = DB::open_default(dbDirectory + &readingFolder).unwrap();
	let mut iter = db.raw_iterator();

	// Forwards iteration
	iter.seek_to_first();
	while iter.valid() {

		//print_type_of(&(iter.key().unwrap()));				// used to get type of a variable

		let lstKey = iter.key().unwrap(); 					// key as a list
		let lstValue = iter.value().unwrap(); 				// value as a list

		let mut lstKeyToHex = Vec::new();
		let mut lstValueToHex = Vec::new();

		let mut keyHex : String = "".to_owned();
		let mut valueHex : String = "".to_owned();
		let zero = "0";

		for i in 0..lstKey.len() {
			lstKeyToHex.push(format!("{:X}", lstKey[i]));		// key list to hex 
			if lstKeyToHex[i].len() == 2 {
				keyHex += &lstKeyToHex[i];
			}
			else {
				keyHex += &zero;								// hex key list to string
				keyHex += &lstKeyToHex[i];
			}
		}

		for i in 0..lstValue.len() {
			lstValueToHex.push(format!("{:X}", lstValue[i]));	// value list to hex 
			if lstValueToHex[i].len() == 2 {
				valueHex += &lstValueToHex[i];
			}
			else {
				valueHex += &zero;								// hex value list to string
				valueHex += &lstValueToHex[i];
			}
		}


		//println!("{:?}\n\n{:?}", lstKey.len(), lstValue.len()); // key and value bytes amount
		println!("{:?}\n\n{:?}", iter.key(), iter.value());		// key and value bytes stored in db
		//println!("{:?}\n\n{:?}", lstKeyToHex, lstValueToHex);	// hex key list and hex value list
		//println!("{:?}\n\n{:?}", keyHex, valueHex);			// hex strings of key and value
		//println!("{:?}\n\n{:?}", lstKey, lstValue);			// key and value lists

		println!("__________________");
		iter.next();
	}

}
